import { DialogClose } from "@radix-ui/react-dialog";
import { PlusSm } from "@styled-icons/heroicons-outline";
import { useState } from "react";
import { TextResources } from "../../../../assets/text";
import { Button } from "../../../../complib/buttons";
import { Dialog } from "../../../../complib/overlays";
import { AttributeInfoCheckbox } from "../../../common/attribute";
import { SearchField } from "../../../common/SearchField";
import { AttributeItem } from "../../../types/AttributeItem";
import { filterAttributeItem, onSelectionChange } from "./SelectAttributeDialog.helpers";
import { SelectAttributesContainer, SelectContainer } from "./SelectAttributeDialog.styled";

interface SelectAttributeDialogProps {
  attributes: AttributeItem[];
  onAdd: (attributeIds: string[]) => void;
}

/**
 * Component which shows a searchable dialog of attributes which from the user can select.
 *
 * @param attributes which the user can select from
 * @param onAdd actions to take when user pressed the add button
 * @constructor
 */
export const SelectAttributeDialog = ({ attributes, onAdd }: SelectAttributeDialogProps) => {
  const [searchQuery, setSearchQuery] = useState("");
  const [selected, setSelected] = useState<string[]>([]);

  const onAddAttributes = () => {
    onAdd(selected);
    setSelected([]);
    setSearchQuery("");
  };

  return (
    <Dialog
      title={TextResources.ATTRIBUTE_DIALOG_TITLE}
      description={TextResources.ATTRIBUTE_DIALOG_DESCRIPTION}
      width={"1000px"}
      content={
        <SelectContainer>
          <SearchField
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            placeholder={TextResources.ATTRIBUTE_DIALOG_SEARCH}
          />
          <SelectAttributesContainer>
            {attributes
              .filter((x) => filterAttributeItem(x, searchQuery))
              .map((a, i) => (
                <AttributeInfoCheckbox
                  key={i}
                  checked={selected.includes(a.id)}
                  onClick={() => onSelectionChange(a.id, selected, setSelected)}
                  {...a}
                />
              ))}
          </SelectAttributesContainer>
          <DialogClose asChild>
            <Button onClick={onAddAttributes} disabled={selected.length < 1}>
              {TextResources.ATTRIBUTE_DIALOG_ADD}
            </Button>
          </DialogClose>
        </SelectContainer>
      }
    >
      <Button icon={<PlusSm />} iconOnly>
        {TextResources.ATTRIBUTE_ADD}
      </Button>
    </Dialog>
  );
};
