import { NodeItem } from "../../../../../content/types/NodeItem";

export const isNodeItem = (item: unknown): item is NodeItem => (<NodeItem>item).kind === "NodeItem";
