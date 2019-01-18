#!/usr/bin/env bash
set -e

aws cloudformation delete-stack --stack-name serverless-showdown-${aws_stage}