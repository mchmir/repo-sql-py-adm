# from http.server import HTTPServer, CGIHTTPRequestHandler
# server_address = ("0.0.0.0", 5000)
# httpd = HTTPServer(server_address, CGIHTTPRequestHandler)
# httpd.serve_forever()


from http.server import HTTPServer, SimpleHTTPRequestHandler
server_address = ("0.0.0.0", 5000)
httpd = HTTPServer(server_address, SimpleHTTPRequestHandler)
httpd.serve_forever()
