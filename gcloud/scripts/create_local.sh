#!/usr/bin/env bash
set -e -u

source ./scripts/local_variables.sh

npm run test
npm run build

cd infra
terraform plan
terraform apply -auto-approve


pwd
sed  -i -e "s^export API_URL=.*^export API_URL=$(terraform output api_url)^" ../scripts/local_variables.sh

