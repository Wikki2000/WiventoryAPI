API_URL="http://localhost:5295/api/auth/login"

curl -s -X POST "$API_URL" \
  -H "Content-Type: application/json" \
  -d '{
  "email": "wisdomokposin@gmail.com"
}' | jq

