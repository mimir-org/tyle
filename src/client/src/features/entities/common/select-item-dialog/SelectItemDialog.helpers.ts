import { InfoItem } from "common/types/infoItem";

/**
 * Simple filtration of an item which tries to match an item's name and descriptors against tokens in search query.
 * Could be replaced by a more advanced client filtering approach or calls against a dedicated search api.
 *
 * @param item
 * @param query
 */
export const filterInfoItem = (item: InfoItem, query: string) => {
  const traitValueString = Object.values(item.descriptors).join(" ");
  const matchTarget = `${item.name} ${traitValueString}`.toLowerCase();
  const queryTokens = query.split(" ");

  return queryTokens.every((t) => matchTarget.includes(t.toLowerCase()));
};

export const onSelectionChange = (
  id: string,
  selected: string[],
  setSelected: (ids: string[]) => void,
  isMultiSelect: boolean,
) => {
  if (selected.includes(id)) {
    setSelected(selected.filter((x) => x !== id));
  } else {
    isMultiSelect ? setSelected([...selected, id]) : setSelected([id]);
  }
};
