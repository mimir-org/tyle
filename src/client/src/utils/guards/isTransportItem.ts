import { TransportItem } from "../../content/types/TransportItem";

export const isTransportItem = (item: unknown): item is TransportItem => (<TransportItem>item).kind === "TransportItem";
