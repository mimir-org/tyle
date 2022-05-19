import { AttributeItem } from "../../content/home/types/AttributeItem";

export const sortAttributes = (attributes: AttributeItem[]) =>
  [...attributes].sort((a, b) => a.name.localeCompare(b.name));
