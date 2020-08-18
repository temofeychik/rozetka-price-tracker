// package: priceTracker
// file: src/app/protos/price_track.proto

import * as jspb from "google-protobuf";
import * as google_protobuf_empty_pb from "google-protobuf/google/protobuf/empty_pb";
import * as google_protobuf_timestamp_pb from "google-protobuf/google/protobuf/timestamp_pb";

export class TrackProductRequest extends jspb.Message {
  getProducturl(): string;
  setProducturl(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): TrackProductRequest.AsObject;
  static toObject(includeInstance: boolean, msg: TrackProductRequest): TrackProductRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: TrackProductRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): TrackProductRequest;
  static deserializeBinaryFromReader(message: TrackProductRequest, reader: jspb.BinaryReader): TrackProductRequest;
}

export namespace TrackProductRequest {
  export type AsObject = {
    producturl: string,
  }
}

export class TrackProcutPricesRequest extends jspb.Message {
  clearProductidsList(): void;
  getProductidsList(): Array<number>;
  setProductidsList(value: Array<number>): void;
  addProductids(value: number, index?: number): number;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): TrackProcutPricesRequest.AsObject;
  static toObject(includeInstance: boolean, msg: TrackProcutPricesRequest): TrackProcutPricesRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: TrackProcutPricesRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): TrackProcutPricesRequest;
  static deserializeBinaryFromReader(message: TrackProcutPricesRequest, reader: jspb.BinaryReader): TrackProcutPricesRequest;
}

export namespace TrackProcutPricesRequest {
  export type AsObject = {
    productidsList: Array<number>,
  }
}

export class TrackProductPriceResponse extends jspb.Message {
  clearProductsList(): void;
  getProductsList(): Array<TrackProductResponse>;
  setProductsList(value: Array<TrackProductResponse>): void;
  addProducts(value?: TrackProductResponse, index?: number): TrackProductResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): TrackProductPriceResponse.AsObject;
  static toObject(includeInstance: boolean, msg: TrackProductPriceResponse): TrackProductPriceResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: TrackProductPriceResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): TrackProductPriceResponse;
  static deserializeBinaryFromReader(message: TrackProductPriceResponse, reader: jspb.BinaryReader): TrackProductPriceResponse;
}

export namespace TrackProductPriceResponse {
  export type AsObject = {
    productsList: Array<TrackProductResponse.AsObject>,
  }
}

export class TrackProductResponse extends jspb.Message {
  getId(): number;
  setId(value: number): void;

  getPrice(): number;
  setPrice(value: number): void;

  getImageurl(): string;
  setImageurl(value: string): void;

  getTitle(): string;
  setTitle(value: string): void;

  getDescription(): string;
  setDescription(value: string): void;

  getUrl(): string;
  setUrl(value: string): void;

  getDiscount(): number;
  setDiscount(value: number): void;

  getStatus(): string;
  setStatus(value: string): void;

  getSellStatus(): string;
  setSellStatus(value: string): void;

  clearAdditionalPricesList(): void;
  getAdditionalPricesList(): Array<ProductAdditionalPricesResponse>;
  setAdditionalPricesList(value: Array<ProductAdditionalPricesResponse>): void;
  addAdditionalPrices(value?: ProductAdditionalPricesResponse, index?: number): ProductAdditionalPricesResponse;

  getPrevPrice(): number;
  setPrevPrice(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): TrackProductResponse.AsObject;
  static toObject(includeInstance: boolean, msg: TrackProductResponse): TrackProductResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: TrackProductResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): TrackProductResponse;
  static deserializeBinaryFromReader(message: TrackProductResponse, reader: jspb.BinaryReader): TrackProductResponse;
}

export namespace TrackProductResponse {
  export type AsObject = {
    id: number,
    price: number,
    imageurl: string,
    title: string,
    description: string,
    url: string,
    discount: number,
    status: string,
    sellStatus: string,
    additionalPricesList: Array<ProductAdditionalPricesResponse.AsObject>,
    prevPrice: number,
  }
}

export class ProductAdditionalPricesResponse extends jspb.Message {
  getId(): number;
  setId(value: number): void;

  getProductId(): number;
  setProductId(value: number): void;

  getDiscountPrice(): number;
  setDiscountPrice(value: number): void;

  getDescription(): string;
  setDescription(value: string): void;

  getTitle(): string;
  setTitle(value: string): void;

  hasLastUpdatedOn(): boolean;
  clearLastUpdatedOn(): void;
  getLastUpdatedOn(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setLastUpdatedOn(value?: google_protobuf_timestamp_pb.Timestamp): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ProductAdditionalPricesResponse.AsObject;
  static toObject(includeInstance: boolean, msg: ProductAdditionalPricesResponse): ProductAdditionalPricesResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: ProductAdditionalPricesResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ProductAdditionalPricesResponse;
  static deserializeBinaryFromReader(message: ProductAdditionalPricesResponse, reader: jspb.BinaryReader): ProductAdditionalPricesResponse;
}

export namespace ProductAdditionalPricesResponse {
  export type AsObject = {
    id: number,
    productId: number,
    discountPrice: number,
    description: string,
    title: string,
    lastUpdatedOn?: google_protobuf_timestamp_pb.Timestamp.AsObject,
  }
}

export class GetProductInfoRequest extends jspb.Message {
  getProductid(): number;
  setProductid(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetProductInfoRequest.AsObject;
  static toObject(includeInstance: boolean, msg: GetProductInfoRequest): GetProductInfoRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: GetProductInfoRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetProductInfoRequest;
  static deserializeBinaryFromReader(message: GetProductInfoRequest, reader: jspb.BinaryReader): GetProductInfoRequest;
}

export namespace GetProductInfoRequest {
  export type AsObject = {
    productid: number,
  }
}

export class PorductPriceHistoryResponse extends jspb.Message {
  getId(): number;
  setId(value: number): void;

  getProductId(): number;
  setProductId(value: number): void;

  getPrice(): number;
  setPrice(value: number): void;

  hasDate(): boolean;
  clearDate(): void;
  getDate(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setDate(value?: google_protobuf_timestamp_pb.Timestamp): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): PorductPriceHistoryResponse.AsObject;
  static toObject(includeInstance: boolean, msg: PorductPriceHistoryResponse): PorductPriceHistoryResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: PorductPriceHistoryResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): PorductPriceHistoryResponse;
  static deserializeBinaryFromReader(message: PorductPriceHistoryResponse, reader: jspb.BinaryReader): PorductPriceHistoryResponse;
}

export namespace PorductPriceHistoryResponse {
  export type AsObject = {
    id: number,
    productId: number,
    price: number,
    date?: google_protobuf_timestamp_pb.Timestamp.AsObject,
  }
}

export class ProductInfoResponse extends jspb.Message {
  hasProductinfo(): boolean;
  clearProductinfo(): void;
  getProductinfo(): TrackProductResponse | undefined;
  setProductinfo(value?: TrackProductResponse): void;

  clearPricesList(): void;
  getPricesList(): Array<PorductPriceHistoryResponse>;
  setPricesList(value: Array<PorductPriceHistoryResponse>): void;
  addPrices(value?: PorductPriceHistoryResponse, index?: number): PorductPriceHistoryResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ProductInfoResponse.AsObject;
  static toObject(includeInstance: boolean, msg: ProductInfoResponse): ProductInfoResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: ProductInfoResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ProductInfoResponse;
  static deserializeBinaryFromReader(message: ProductInfoResponse, reader: jspb.BinaryReader): ProductInfoResponse;
}

export namespace ProductInfoResponse {
  export type AsObject = {
    productinfo?: TrackProductResponse.AsObject,
    pricesList: Array<PorductPriceHistoryResponse.AsObject>,
  }
}

