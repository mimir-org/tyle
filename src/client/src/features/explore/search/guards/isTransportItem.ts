import { TransportItem } from "common/types/transportItem";

export const isTransportItem = (item: unknown): item is TransportItem => (<TransportItem>item).kind === "TransportItem";
