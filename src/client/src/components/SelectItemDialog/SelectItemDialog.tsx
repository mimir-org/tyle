import { DialogClose } from "@radix-ui/react-dialog";
import { PlusSmall } from "@styled-icons/heroicons-outline";
import { InfoItem } from "common/types/infoItem";
import { Button, Dialog } from "@mimirorg/component-library";
import { InfoItemCheckbox } from "components/InfoItemCheckbox/InfoItemCheckbox";
import { SearchField } from "components/SearchField";
import { filterInfoItem, onSelectionChange } from "components/SelectItemDialog/SelectItemDialog.helpers";
import { SelectContainer, SelectItemsContainer } from "components/SelectItemDialog/SelectItemDialog.styled";
import { useState } from "react";

interface SelectItemDialogProps {
  title: string;
  description: string;
  searchFieldText: string;
  addItemsButtonText: string;
  openDialogButtonText: string;
  items: InfoItem[];
  onAdd: (ids: string[]) => void;
  isMultiSelect?: boolean;
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
const SelectItemDialog = ({
  title,
  description,
  searchFieldText,
  addItemsButtonText,
  openDialogButtonText,
  items,
  onAdd,
  isMultiSelect = true,
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
                  key={a?.id ?? i}
                  checked={selected.includes(a?.id ?? "")}
                  onClick={() => onSelectionChange(a?.id ?? "", selected, setSelected, isMultiSelect)}
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
      <Button icon={<PlusSmall />} iconOnly>
        {openDialogButtonText}
      </Button>
    </Dialog>
  );
};

export default SelectItemDialog;