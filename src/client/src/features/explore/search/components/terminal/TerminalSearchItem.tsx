import { State } from "@mimirorg/typelibrary-types";
import { Duplicate, PencilAlt, Trash } from "@styled-icons/heroicons-outline";
import { TerminalItem } from "common/types/terminalItem";
import { Button } from "complib/buttons";
import { AlertDialog } from "complib/overlays";
import { usePatchTerminalState } from "external/sources/terminal/terminal.queries";
import { PlainLink } from "features/common/plain-link";
import { TerminalPreview } from "features/common/terminal/TerminalPreview";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export type TerminalSearchItemProps = TerminalItem & {
  isSelected?: boolean;
  setSelected?: () => void;
};

/**
 * Component which visualizes a terminal search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param terminal
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
  const { t } = useTranslation("explore");

  const deleteMutation = usePatchTerminalState();
  const deleteAction = {
    name: t("search.item.delete"),
    onAction: () => deleteMutation.mutate({ id, state: State.Delete }),
  };

  return (
    <>
      <PlainLink tabIndex={-1} to={`/form/terminal/clone/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<Duplicate />} iconOnly>
          {t("search.item.clone")}
        </Button>
      </PlainLink>
      <PlainLink tabIndex={-1} to={`/form/terminal/edit/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<PencilAlt />} iconOnly>
          {t("search.item.edit")}
        </Button>
      </PlainLink>
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[deleteAction]}
        title={t("search.item.templates.delete", { object: name })}
        description={t("search.item.deleteDescription")}
        hideDescription
        content={<TerminalPreview name={name} {...rest} />}
      >
        <Button icon={<Trash />} iconOnly>
          {t("search.item.delete")}
        </Button>
      </AlertDialog>
    </>
  );
};
