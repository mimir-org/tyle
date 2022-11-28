import { State } from "@mimirorg/typelibrary-types";
import { Duplicate, PencilAlt, Trash } from "@styled-icons/heroicons-outline";
import { InterfaceItem } from "common/types/interfaceItem";
import { Button } from "complib/buttons";
import { AlertDialog } from "complib/overlays";
import { usePatchInterfaceState } from "external/sources/interface/interface.queries";
import { InterfacePreview } from "features/common/interface";
import { PlainLink } from "features/common/plain-link";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export type InterfaceSearchItemProps = InterfaceItem & {
  isSelected?: boolean;
  setSelected?: () => void;
};

/**
 * Component which visualizes a interface search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param interfaceItem
 * @constructor
 */
export const InterfaceSearchItem = ({ isSelected, setSelected, ...interfaceItem }: InterfaceSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<InterfacePreview {...interfaceItem} />}
    description={<ItemDescription onClick={setSelected} {...interfaceItem} />}
    actions={<InterfaceSearchItemActions {...interfaceItem} />}
  />
);

const InterfaceSearchItemActions = ({ id, name, ...rest }: InterfaceItem) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "search.item" });

  const deleteMutation = usePatchInterfaceState();
  const deleteAction = {
    name: t("delete"),
    onAction: () => deleteMutation.mutate({ id, state: State.Delete }),
  };

  return (
    <>
      <PlainLink tabIndex={-1} to={`/form/interface/clone/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<Duplicate />} iconOnly>
          {t("clone")}
        </Button>
      </PlainLink>
      <PlainLink tabIndex={-1} to={`/form/interface/edit/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<PencilAlt />} iconOnly>
          {t("edit")}
        </Button>
      </PlainLink>
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[deleteAction]}
        title={t("templates.delete", { object: name })}
        description={t("deleteDescription")}
        hideDescription
        content={<InterfacePreview name={name} {...rest} />}
      >
        <Button icon={<Trash />} iconOnly>
          {t("delete")}
        </Button>
      </AlertDialog>
    </>
  );
};
