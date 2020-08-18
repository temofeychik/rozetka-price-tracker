// package: priceTracker
// file: src/app/protos/price_track.proto

var src_app_protos_price_track_pb = require("../../../src/app/protos/price_track_pb");
var google_protobuf_empty_pb = require("google-protobuf/google/protobuf/empty_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var PriceTracker = (function () {
  function PriceTracker() {}
  PriceTracker.serviceName = "priceTracker.PriceTracker";
  return PriceTracker;
}());

PriceTracker.TrackProduct = {
  methodName: "TrackProduct",
  service: PriceTracker,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_price_track_pb.TrackProductRequest,
  responseType: src_app_protos_price_track_pb.TrackProductResponse
};

PriceTracker.TrackPrices = {
  methodName: "TrackPrices",
  service: PriceTracker,
  requestStream: false,
  responseStream: false,
  requestType: google_protobuf_empty_pb.Empty,
  responseType: src_app_protos_price_track_pb.TrackProductPriceResponse
};

PriceTracker.TrackProductStream = {
  methodName: "TrackProductStream",
  service: PriceTracker,
  requestStream: false,
  responseStream: true,
  requestType: src_app_protos_price_track_pb.TrackProductRequest,
  responseType: src_app_protos_price_track_pb.TrackProductResponse
};

PriceTracker.GetProductInfo = {
  methodName: "GetProductInfo",
  service: PriceTracker,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_price_track_pb.GetProductInfoRequest,
  responseType: src_app_protos_price_track_pb.ProductInfoResponse
};

exports.PriceTracker = PriceTracker;

function PriceTrackerClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

PriceTrackerClient.prototype.trackProduct = function trackProduct(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(PriceTracker.TrackProduct, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

PriceTrackerClient.prototype.trackPrices = function trackPrices(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(PriceTracker.TrackPrices, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

PriceTrackerClient.prototype.trackProductStream = function trackProductStream(requestMessage, metadata) {
  var listeners = {
    data: [],
    end: [],
    status: []
  };
  var client = grpc.invoke(PriceTracker.TrackProductStream, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onMessage: function (responseMessage) {
      listeners.data.forEach(function (handler) {
        handler(responseMessage);
      });
    },
    onEnd: function (status, statusMessage, trailers) {
      listeners.status.forEach(function (handler) {
        handler({ code: status, details: statusMessage, metadata: trailers });
      });
      listeners.end.forEach(function (handler) {
        handler({ code: status, details: statusMessage, metadata: trailers });
      });
      listeners = null;
    }
  });
  return {
    on: function (type, handler) {
      listeners[type].push(handler);
      return this;
    },
    cancel: function () {
      listeners = null;
      client.close();
    }
  };
};

PriceTrackerClient.prototype.getProductInfo = function getProductInfo(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(PriceTracker.GetProductInfo, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.PriceTrackerClient = PriceTrackerClient;

