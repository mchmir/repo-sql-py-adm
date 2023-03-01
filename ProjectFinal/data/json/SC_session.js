{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "type": "object",
  "properties": {
  "patternProperties": {
    "^[0-9]{4}-[0-9]{2}-[0-9]{2}$": {
    "type": "array" }
    }
      "items": {
        "type": "object",
        "properties": {
          "session_id": {"type": "string"},
          "client_id": {"type": "string"},
          "visit_date": {"type": "string", "format": "date"},
          "visit_time": {"type": "string", "format": "time"},
          "visit_number": {"type": "integer"},
          "utm_source": {"type": "string"},
          "utm_medium": {"type": "string"},
          "utm_campaign": {"type": "string", "nullable": true},
          "utm_adcontent": {"type": "string"},
          "utm_keyword": {"type": "string"},
          "device_category": {"type": "string"},
          "device_os": {"type": "string"},
          "device_brand": {"type": "string"},
          "device_model": {"type": "string", "nullable": true},
          "device_screen_resolution": {"type": "string"},
          "device_browser": {"type": "string"},
          "geo_country": {"type": "string"},
          "geo_city": {"type": "string"}
        },
        "required": ["session_id", "client_id", "visit_date", "visit_time", "visit_number", "utm_source", "utm_medium", "utm_adcontent", "utm_keyword", "device_category", "device_os", "device_brand", "device_screen_resolution", "device_browser", "geo_country", "geo_city"]
      }
  },
  "required": ["^[0-9]{4}-[0-9]{2}-[0-9]{2}$"]
}