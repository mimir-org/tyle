import { Duplicate, PencilAlt, Trash } from "@styled-icons/heroicons-outline";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Button } from "../../../../../../complib/buttons";
import { AlertDialog } from "../../../../../../complib/overlays/alert-dialog/AlertDialog";
import { AttributePreview } from "../../../../../common/attribute";
import { AttributeItem } from "../../../../../types/AttributeItem";
import { PlainLink } from "../../../../../utils/PlainLink";
import { Item } from "../item/Item";
import { ItemDescription } from "../item/ItemDescription";

export type AttributeSearchItemProps = AttributeItem & {
  isSelected?: boolean;
  setSelected?: () => void;
};

/**
 * Component which visualizes a single attribute search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param attribute
 * @constructor
 */
export const AttributeSearchItem = ({ isSelected, setSelected, ...attribute }: AttributeSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<AttributePreview {...attribute} />}
    description={<ItemDescription onClick={setSelected} {...attribute} />}
    actions={<AttributeSearchItemActions {...attribute} />}
  />
);

const AttributeSearchItemActions = ({ id, name, ...rest }: AttributeItem) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "search.item" });

  return (
    <>
      <PlainLink tabIndex={-1} to={`/form/attribute/clone/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<Duplicate />} iconOnly>
          {t("clone")}
        </Button>
      </PlainLink>
      <Button icon={<PencilAlt />} iconOnly disabled>
        {t("edit")}
      </Button>
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[]}
        title={t("templates.delete", { object: name })}
        description={t("deleteDescription")}
        hideDescription
        content={<AttributePreview name={name} {...rest} />}
      >
        <Button icon={<Trash />} iconOnly disabled>
          {t("delete")}
        </Button>
      </AlertDialog>
    </>
  );
};
