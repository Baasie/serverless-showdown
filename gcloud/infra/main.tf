variable "app_name" {}

provider "google" {
  credentials = "${file("CREDENTIALS_FILE.json")}"
  project     = "speeltuin-kenny-baas"
  region      = "europe-west1"
}


terraform {
  backend "local" {
    path = "./state/terraform.tfstate"
  }
  required_version = "= 0.11.11"
}

resource "google_cloudfunctions_function" "test" {
  name                      = "request-car-entry"
  entry_point               = "handle"
  available_memory_mb       = 128
  timeout                   = 61
  project                   = "${var.app_name}"
  region                    = "us-central1"
  trigger_http              = true
  source_archive_bucket     = "${google_storage_bucket.bucket.name}"
  source_archive_object     = "${google_storage_bucket_object.archive.name}"
  labels {
    deployment_name           = "test"
  }
}

resource "google_storage_bucket" "bucket" {
  name = "showdown-serverless-deploy"
}

data "archive_file" "http_trigger" {
  type        = "zip"
  output_path = "./../dist/index.zip"
  source {
    content  = "${file("./../dist/request-car-entry.js")}"
    filename = "index.js"
  }
}

resource "google_storage_bucket_object" "archive" {
  name   = "http_trigger.zip"
  bucket = "${google_storage_bucket.bucket.name}"
  source = "./../dist/index.zip"
  depends_on = ["data.archive_file.http_trigger"]
}
