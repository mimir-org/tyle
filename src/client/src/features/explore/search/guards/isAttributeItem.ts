import { AttributeItem } from "../../../../common/types/attributeItem";
export const isAttributeItem = (item: unknown): item is AttributeItem =>
  (<AttributeItem>item).kind === "AttributeLibCm";
