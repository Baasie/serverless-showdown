#!/usr/bin/env bash
set -e -u

source ./scripts/local_variables.sh

cd infra
terraform destroy --force -var account_id=${AWS_ACCOUNT_ID} -var region=${AWS_REGION} -var stage=${aws_stage}

cd ..