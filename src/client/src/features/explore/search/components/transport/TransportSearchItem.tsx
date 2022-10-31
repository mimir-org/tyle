import { State } from "@mimirorg/typelibrary-types";
import { Duplicate, PencilAlt, Trash } from "@styled-icons/heroicons-outline";
import { PlainLink } from "common/components/plain-link";
import { TransportPreview } from "common/components/transport";
import { TransportItem } from "common/types/transportItem";
import { Button } from "complib/buttons";
import { AlertDialog } from "complib/overlays";
import { usePatchTransportState } from "external/sources/transport/transport.queries";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export type TransportSearchItemProps = TransportItem & {
  isSelected?: boolean;
  setSelected?: () => void;
};

/**
 * Component which visualizes a transport search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param transport
 * @constructor
 */
export const TransportSearchItem = ({ isSelected, setSelected, ...transport }: TransportSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<TransportPreview {...transport} />}
    description={<ItemDescription onClick={setSelected} {...transport} />}
    actions={<TransportSearchItemActions {...transport} />}
  />
);

const TransportSearchItemActions = ({ id, name, ...rest }: TransportItem) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "search.item" });

  const deleteMutation = usePatchTransportState();
  const deleteAction = {
    name: t("delete"),
    onAction: () => deleteMutation.mutate({ id, state: State.Delete }),
  };

  return (
    <>
      <PlainLink tabIndex={-1} to={`/form/transport/clone/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<Duplicate />} iconOnly>
          {t("clone")}
        </Button>
      </PlainLink>
      <PlainLink tabIndex={-1} to={`/form/transport/edit/${id}`}>
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
        content={<TransportPreview name={name} {...rest} />}
      >
        <Button icon={<Trash />} iconOnly>
          {t("delete")}
        </Button>
      </AlertDialog>
    </>
  );
};
