package main

import (
	"app/typhenapi/core"
	webapi "app/typhenapi/web/submarine"
	"net/http"
)

// WebAPIRoundTripper for mock.
var WebAPIRoundTripper http.RoundTripper

// NewWebAPI creates a submarine WebAPI instance.
func NewWebAPI(baseURI string) *webapi.WebAPI {
	serializer := typhenapi.NewJSONSerializer()
	api := webapi.New(baseURI, serializer, nil)
	api.Client.Transport = WebAPIRoundTripper
	api.BeforeRequestHandler = onBeforeWebAPIRequest
	return api
}

func onBeforeWebAPIRequest(req *http.Request) {
	req.Header.Add("Content-Type", "application/json")
}