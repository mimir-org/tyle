import { AttributeItem } from "../../content/types/AttributeItem";

export const isAttributeItem = (item: unknown): item is AttributeItem => (<AttributeItem>item).kind === "AttributeItem";
