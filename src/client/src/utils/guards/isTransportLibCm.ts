import { TransportLibCm } from "@mimirorg/typelibrary-types";

export const isTransportLibCm = (item: unknown): item is TransportLibCm =>
  (<TransportLibCm>item).kind === "TransportLibCm";
