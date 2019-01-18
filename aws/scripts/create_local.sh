#!/usr/bin/env bash
set -e -u

export AWS_REGION="eu-west-1"
export aws_stage="kb-local"

#npm run test
npm run build

aws cloudformation package --region eu-west-1 --template-file template.yaml --s3-bucket baasie.fs.release --s3-prefix ${aws_stage} --output-template-file packaged.yaml
aws cloudformation deploy --region eu-west-1 --template-file ./packaged.yaml --stack-name serverless-showdown-${aws_stage} --parameter-overrides Stage=${aws_stage} --capabilities CAPABILITY_IAM

pwd
sed  -i -e "s^export API_URL=.*^export API_URL=$(aws cloudformation describe-stacks --stack-name serverless-showdown-$aws_stage --query 'Stacks[0].Outputs[?OutputKey==`ParkingGarageApi`].OutputValue' --output text)^" ./scripts/local_variables.sh
