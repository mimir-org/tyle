import { DialogClose } from "@radix-ui/react-dialog";
import { PlusSm } from "@styled-icons/heroicons-outline";
import { useState } from "react";
import { Button } from "../../../../complib/buttons";
import { Dialog } from "../../../../complib/overlays";
import { InfoItemCheckbox } from "../../../common/info-item";
import { SearchField } from "../../../common/search-field";
import { InfoItem } from "../../../types/InfoItem";
import { filterInfoItem, onSelectionChange } from "./SelectItemDialog.helpers";
import { SelectContainer, SelectItemsContainer } from "./SelectItemDialog.styled";

interface SelectItemDialogProps {
  title: string;
  description: string;
  searchFieldText: string;
  addItemsButtonText: string;
  openDialogButtonText: string;
  items: InfoItem[];
  onAdd: (ids: string[]) => void;
}

/**
 * Component which shows a searchable dialog of items which from the user can select.
 *
 * @param title
 * @param description
 * @param searchFieldText
 * @param addItemsButtonText
 * @param openDialogButtonText
 * @param items which the user can select from
 * @param onAdd actions to take when user pressed the add button
 * @constructor
 */
export const SelectItemDialog = ({
  title,
  description,
  searchFieldText,
  addItemsButtonText,
  openDialogButtonText,
  items,
  onAdd,
}: SelectItemDialogProps) => {
  const [searchQuery, setSearchQuery] = useState("");
  const [selected, setSelected] = useState<string[]>([]);

  const onAddItems = () => {
    onAdd(selected);
    setSelected([]);
    setSearchQuery("");
  };

  return (
    <Dialog
      title={title}
      description={description}
      width={"1000px"}
      content={
        <SelectContainer>
          <SearchField
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            placeholder={searchFieldText}
          />
          <SelectItemsContainer>
            {items
              .filter((x) => filterInfoItem(x, searchQuery))
              .map((a, i) => (
                <InfoItemCheckbox
                  key={i}
                  checked={selected.includes(a?.id ?? "")}
                  onClick={() => onSelectionChange(a?.id ?? "", selected, setSelected)}
                  {...a}
                />
              ))}
          </SelectItemsContainer>
          <DialogClose asChild>
            <Button onClick={onAddItems} disabled={selected.length < 1}>
              {addItemsButtonText}
            </Button>
          </DialogClose>
        </SelectContainer>
      }
    >
      <Button icon={<PlusSm />} iconOnly>
        {openDialogButtonText}
      </Button>
    </Dialog>
  );
};
