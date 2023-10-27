import { BlockItem } from "../../../../common/types/blockItem";

export const isBlockItem = (item: unknown): item is BlockItem => (<BlockItem>item).kind === "BlockItem";
