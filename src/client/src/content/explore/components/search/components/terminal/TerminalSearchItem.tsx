import { Duplicate, PencilAlt, Trash } from "@styled-icons/heroicons-outline";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Button } from "../../../../../../complib/buttons";
import { AlertDialog } from "../../../../../../complib/overlays/alert-dialog/AlertDialog";
import { TerminalPreview } from "../../../../../common/terminal/TerminalPreview";
import { TerminalItem } from "../../../../../types/TerminalItem";
import { PlainLink } from "../../../../../utils/PlainLink";
import { Item } from "../item/Item";
import { ItemDescription } from "../item/ItemDescription";

export type TerminalSearchItemProps = TerminalItem & {
  isSelected?: boolean;
  setSelected?: () => void;
};

/**
 * Component which visualizes a terminal search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param attribute
 * @constructor
 */
export const TerminalSearchItem = ({ isSelected, setSelected, ...terminal }: TerminalSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<TerminalPreview {...terminal} />}
    description={<ItemDescription onClick={setSelected} {...terminal} />}
    actions={<TerminalSearchItemActions {...terminal} />}
  />
);

const TerminalSearchItemActions = ({ id, name, ...rest }: TerminalItem) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "search.item" });

  return (
    <>
      <PlainLink tabIndex={-1} to={`/form/terminal/clone/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<Duplicate />} iconOnly>
          {t("clone")}
        </Button>
      </PlainLink>
      <PlainLink tabIndex={-1} to={`/form/terminal/edit/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<PencilAlt />} iconOnly>
          {t("edit")}
        </Button>
      </PlainLink>
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[]}
        title={t("templates.delete", { object: name })}
        description={t("deleteDescription")}
        hideDescription
        content={<TerminalPreview name={name} {...rest} />}
      >
        <Button icon={<Trash />} iconOnly disabled>
          {t("delete")}
        </Button>
      </AlertDialog>
    </>
  );
};
