import { State } from "@mimirorg/typelibrary-types";
import { Duplicate, PencilAlt, Trash, ChevronUp, ChevronDoubleUp } from "@styled-icons/heroicons-outline";
import { TransportItem } from "common/types/transportItem";
import { UserItem } from "common/types/userItem";
import { Button } from "complib/buttons";
import { AlertDialog } from "complib/overlays";
import { usePatchTransportState } from "external/sources/transport/transport.queries";
import { PlainLink } from "features/common/plain-link";
import { TransportPreview } from "features/common/transport";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { useButtonStateFilter } from "features/explore/search/hooks/useButtonFilter";

export type TransportSearchItemProps = TransportItem & {
  isSelected?: boolean;
  setSelected?: () => void;
  user: UserItem;
};

/**
 * Component which visualizes a transport search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param transport
 * @param user
 * @constructor
 */
export const TransportSearchItem = ({ isSelected, setSelected, user, ...transport }: TransportSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<TransportPreview {...transport} />}
    description={<ItemDescription onClick={setSelected} {...transport} />}
    actions={<TransportSearchItemActions user={user} transport={transport} />}
  />
);

type TransportSearchItemActionProps = {
  user: UserItem
  transport?: TransportItem;
};

const TransportSearchItemActions = ({ user, transport }: TransportSearchItemActionProps) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");
  const patcMutation = usePatchTransportState();
  const btnFilter = useButtonStateFilter(transport ?? null, user);

  if(user == null || transport == null)
    return(<></>);

  const deleteAction = {
    name: t("search.item.delete"),
    onAction: () => patcMutation.mutate({ id: transport.id, state: State.Delete }),
  };

  const approveCompanyAction = {
    name: t("search.item.approve"),
    onAction: () => patcMutation.mutate({ id: transport.id, state: State.ApproveCompany }),
  };

  const approveGlobalAction = {
    name: t("search.item.approve"),
    onAction: () => patcMutation.mutate({ id: transport.id, state: State.ApproveGlobal }),
  };
  
  const cloneLink = btnFilter.clone ? `/form/transport/clone/${transport.id}` : "#";
  const editLink = btnFilter.edit ? `/form/transport/edit/${transport.id}` : "#";

  return (
    <>
      <PlainLink tabIndex={-1} to={cloneLink}>
        <Button disabled={!btnFilter.clone} tabIndex={0} as={!btnFilter.clone ? "button" : "span"} icon={<Duplicate />} iconOnly>
          {t("search.item.clone")}
        </Button>
      </PlainLink>
      <PlainLink tabIndex={-1} to={editLink}>
        <Button disabled={!btnFilter.edit} tabIndex={0} as={!btnFilter.edit ? "button" : "span"} icon={<PencilAlt />} iconOnly>
          {t("search.item.edit")}
        </Button>
      </PlainLink>
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[deleteAction]}
        title={t("search.item.templates.delete", { object: name })}
        description={t("search.item.deleteDescription")}
        hideDescription
        content={<TransportPreview name={transport.name} aspectColor={transport.aspectColor} transportColor={transport.transportColor} />}
      >
        <Button disabled={!btnFilter.delete} icon={<Trash />} iconOnly>
          {t("search.item.delete")}
        </Button>
      </AlertDialog>

      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[approveCompanyAction]}
        title={t("search.item.templates.approveCompany")}
        description={t("search.item.approveDescription")}
        hideDescription
        content={<TransportPreview name={transport.name} aspectColor={transport.aspectColor} transportColor={transport.transportColor} />}
      >
        <Button disabled={!btnFilter.approveCompany} icon={<ChevronUp />} iconOnly>{t("search.item.approve")}</Button>
      </AlertDialog>

      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[approveGlobalAction]}
        title={t("search.item.templates.approveGlobal")}
        description={t("search.item.approveDescription")}
        hideDescription
        content={<TransportPreview name={transport.name} aspectColor={transport.aspectColor} transportColor={transport.transportColor} />}
      >
        <Button disabled={!btnFilter.approveGlobal} icon={<ChevronDoubleUp />} iconOnly>{t("search.item.approve")}</Button>
      </AlertDialog>
    </>
  );
};
