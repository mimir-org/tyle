import { AttributeItem } from "../../../home/types/AttributeItem";

/**
 * Simple filtration of an attribute which tries to match an attribute's name and trait values against tokens in search query.
 * Could be replaced by a more advanced client filtering approach or calls against a dedicated search api.
 *
 * @param attribute
 * @param query
 */
export const filterAttributeItem = (attribute: AttributeItem, query: string) => {
  const traitValueString = Object.values(attribute.traits).join(" ");
  const matchTarget = `${attribute.name} ${traitValueString}`.toLowerCase();
  const queryTokens = query.split(" ");

  return queryTokens.every((t) => matchTarget.includes(t.toLowerCase()));
};

export const onSelectionChange = (id: string, selected: string[], setSelected: (ids: string[]) => void) => {
  if (selected.includes(id)) {
    setSelected(selected.filter((x) => x !== id));
  } else {
    setSelected([...selected, id]);
  }
};
