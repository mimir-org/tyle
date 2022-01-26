#!/usr/bin/env sh

execute_injection() {
  # Injects custom environment variables into index.html.
  # 1. Read in prefix (e.g. MIMIR_ENV), placeholder (where to inject, e.g. __MIMIR_ENV__)
  #    and file_to_inject (e.g. index.html) as arguments.
  # 2. Read all environment variables containing prefix.
  # 3. Build json string, containing key, value pairs of the environment variable name (stripping away MIMIR_ENV_) and its value.
  #    e.g. MIMIR_ENV_API_URL=localhost:5000 -> API_URL: 'localhost:5000'.
  #    When transforming the key value pair, it is important to remember that the value might contain "=", so we need to match against first occurence of the character,
  #    e.g. ([^=]*).
  # 4. Inject variables into file file_to_inject, location specified by placeholder.

  prefix=$1
  placeholder=$2
  file_to_inject=$3

  get_env_variables() {
    list=$(printenv | grep "^$prefix")
  }

  build_json() {
    json="{ "

    for var in $list; do
      json="$json $(echo $var | sed -r -e "s/${prefix}_([^=]*)=(.*)/\1: '\2'/g"), "
    done

    json="${json%??} }"
  }

  inject_env_variables() {
    sed -i "s~\"$placeholder\"~$json~g" $file_to_inject
  }

  get_env_variables

  build_json

  inject_env_variables

}

start_nginx() {
  nginx -g "daemon off;"
}

execute_injection $1 $2 $3
start_nginx
