import { State } from "@mimirorg/typelibrary-types";
import { Duplicate, PencilAlt, Trash, ChevronUp, ChevronDoubleUp } from "@styled-icons/heroicons-outline";
import { InterfaceItem } from "common/types/interfaceItem";
import { UserItem } from "common/types/userItem";
import { Button } from "complib/buttons";
import { AlertDialog } from "complib/overlays";
import { usePatchInterfaceState } from "external/sources/interface/interface.queries";
import { InterfacePreview } from "features/common/interface";
import { PlainLink } from "features/common/plain-link";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { useButtonStateFilter } from "features/explore/search/hooks/useButtonFilter";

export type InterfaceSearchItemProps = InterfaceItem & {
  isSelected?: boolean;
  setSelected?: () => void;
  user: UserItem;
};

/**
 * Component which visualizes a interface search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param interfaceItem
 * @param user
 * @constructor
 */
export const InterfaceSearchItem = ({ isSelected, setSelected, user, ...interfaceItem }: InterfaceSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<InterfacePreview {...interfaceItem} />}
    description={<ItemDescription onClick={setSelected} {...interfaceItem} />}
    actions={<InterfaceSearchItemActions user={user} iface={interfaceItem} />}
  />
);

type InterfaceSearchItemActionProps = {
  user: UserItem
  iface?: InterfaceItem;
};

const InterfaceSearchItemActions = ({ user, iface }: InterfaceSearchItemActionProps) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");
  const patcMutation = usePatchInterfaceState();
  const btnFilter = useButtonStateFilter(iface ?? null, user);
  
  if(user == null || iface == null)
  return(<></>);

  const deleteAction = {
    name: t("search.item.delete"),
    onAction: () => patcMutation.mutate({ id: iface.id, state: State.Delete }),
  };

  const approveCompanyAction = {
    name: t("search.item.approve"),
    onAction: () => patcMutation.mutate({ id: iface.id, state: State.ApproveCompany }),
  };

  const approveGlobalAction = {
    name: t("search.item.approve"),
    onAction: () => patcMutation.mutate({ id: iface.id, state: State.ApproveGlobal }),
  };

  const cloneLink = btnFilter.clone ? `/form/interface/clone/${iface.id}` : "#";
  const editLink = btnFilter.edit ? `/form/interface/edit/${iface.id}` : "#";

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
        content={<InterfacePreview name={iface.name} aspectColor={iface.aspectColor} interfaceColor={iface.interfaceColor} />}
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
        content={<InterfacePreview name={iface.name} aspectColor={iface.aspectColor} interfaceColor={iface.interfaceColor} />}
      >
        <Button disabled={!btnFilter.approveCompany} icon={<ChevronUp />} iconOnly>{t("search.item.approve")}</Button>
      </AlertDialog>

      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[approveGlobalAction]}
        title={t("search.item.templates.approveGlobal")}
        description={t("search.item.approveDescription")}
        hideDescription
        content={<InterfacePreview name={iface.name} aspectColor={iface.aspectColor} interfaceColor={iface.interfaceColor} />}
      >
        <Button disabled={!btnFilter.approveGlobal} icon={<ChevronDoubleUp />} iconOnly>{t("search.item.approve")}</Button>
      </AlertDialog>
    </>
  );
};
