#!/usr/bin/env bash
ktmpl "Jupiter/deployment.yml" -p tag "${BUILDKITE_BUILD_NUMBER}" | kubectl apply -f - -n fma