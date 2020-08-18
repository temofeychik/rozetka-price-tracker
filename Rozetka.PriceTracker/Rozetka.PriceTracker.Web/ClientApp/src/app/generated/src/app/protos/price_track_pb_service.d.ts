// package: priceTracker
// file: src/app/protos/price_track.proto

import * as src_app_protos_price_track_pb from "../../../src/app/protos/price_track_pb";
import * as google_protobuf_empty_pb from "google-protobuf/google/protobuf/empty_pb";
import {grpc} from "@improbable-eng/grpc-web";

type PriceTrackerTrackProduct = {
  readonly methodName: string;
  readonly service: typeof PriceTracker;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_price_track_pb.TrackProductRequest;
  readonly responseType: typeof src_app_protos_price_track_pb.TrackProductResponse;
};

type PriceTrackerTrackPrices = {
  readonly methodName: string;
  readonly service: typeof PriceTracker;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof google_protobuf_empty_pb.Empty;
  readonly responseType: typeof src_app_protos_price_track_pb.TrackProductPriceResponse;
};

type PriceTrackerTrackProductStream = {
  readonly methodName: string;
  readonly service: typeof PriceTracker;
  readonly requestStream: false;
  readonly responseStream: true;
  readonly requestType: typeof src_app_protos_price_track_pb.TrackProductRequest;
  readonly responseType: typeof src_app_protos_price_track_pb.TrackProductResponse;
};

type PriceTrackerGetProductInfo = {
  readonly methodName: string;
  readonly service: typeof PriceTracker;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_price_track_pb.GetProductInfoRequest;
  readonly responseType: typeof src_app_protos_price_track_pb.ProductInfoResponse;
};

export class PriceTracker {
  static readonly serviceName: string;
  static readonly TrackProduct: PriceTrackerTrackProduct;
  static readonly TrackPrices: PriceTrackerTrackPrices;
  static readonly TrackProductStream: PriceTrackerTrackProductStream;
  static readonly GetProductInfo: PriceTrackerGetProductInfo;
}

export type ServiceError = { message: string, code: number; metadata: grpc.Metadata }
export type Status = { details: string, code: number; metadata: grpc.Metadata }

interface UnaryResponse {
  cancel(): void;
}
interface ResponseStream<T> {
  cancel(): void;
  on(type: 'data', handler: (message: T) => void): ResponseStream<T>;
  on(type: 'end', handler: (status?: Status) => void): ResponseStream<T>;
  on(type: 'status', handler: (status: Status) => void): ResponseStream<T>;
}
interface RequestStream<T> {
  write(message: T): RequestStream<T>;
  end(): void;
  cancel(): void;
  on(type: 'end', handler: (status?: Status) => void): RequestStream<T>;
  on(type: 'status', handler: (status: Status) => void): RequestStream<T>;
}
interface BidirectionalStream<ReqT, ResT> {
  write(message: ReqT): BidirectionalStream<ReqT, ResT>;
  end(): void;
  cancel(): void;
  on(type: 'data', handler: (message: ResT) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'end', handler: (status?: Status) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'status', handler: (status: Status) => void): BidirectionalStream<ReqT, ResT>;
}

export class PriceTrackerClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  trackProduct(
    requestMessage: src_app_protos_price_track_pb.TrackProductRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_price_track_pb.TrackProductResponse|null) => void
  ): UnaryResponse;
  trackProduct(
    requestMessage: src_app_protos_price_track_pb.TrackProductRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_price_track_pb.TrackProductResponse|null) => void
  ): UnaryResponse;
  trackPrices(
    requestMessage: google_protobuf_empty_pb.Empty,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_price_track_pb.TrackProductPriceResponse|null) => void
  ): UnaryResponse;
  trackPrices(
    requestMessage: google_protobuf_empty_pb.Empty,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_price_track_pb.TrackProductPriceResponse|null) => void
  ): UnaryResponse;
  trackProductStream(requestMessage: src_app_protos_price_track_pb.TrackProductRequest, metadata?: grpc.Metadata): ResponseStream<src_app_protos_price_track_pb.TrackProductResponse>;
  getProductInfo(
    requestMessage: src_app_protos_price_track_pb.GetProductInfoRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_price_track_pb.ProductInfoResponse|null) => void
  ): UnaryResponse;
  getProductInfo(
    requestMessage: src_app_protos_price_track_pb.GetProductInfoRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_price_track_pb.ProductInfoResponse|null) => void
  ): UnaryResponse;
}

