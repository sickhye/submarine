package server

import (
	"net/http"

	"github.com/shiwano/submarine/server/battle/lib/typhenapi/core"
	webapi "github.com/shiwano/submarine/server/battle/lib/typhenapi/web/submarine"
)

// WebAPIRoundTripper for mock.
var WebAPIRoundTripper http.RoundTripper

func newWebAPI(baseURI string) *webapi.WebAPI {
	serializer := typhenapi.NewJSONSerializer()
	api := webapi.New(baseURI, serializer, nil)
	api.Client.Transport = WebAPIRoundTripper
	api.SetBeforeRequestHandler(onBeforeWebAPIRequest)
	return api
}

func onBeforeWebAPIRequest(req *http.Request) {
	req.Header.Add("Content-Type", "application/json")
}
