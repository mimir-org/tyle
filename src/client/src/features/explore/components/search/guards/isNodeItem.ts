import { NodeItem } from "../../../../../common/types/nodeItem";

export const isNodeItem = (item: unknown): item is NodeItem => (<NodeItem>item).kind === "NodeItem";
