import { AttributeItem } from "../../content/types/AttributeItem";

export const sortAttributes = (attributes: AttributeItem[]) =>
  [...attributes].sort((a, b) => a.name.localeCompare(b.name));
