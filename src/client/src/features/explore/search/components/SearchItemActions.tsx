import { useTheme } from "styled-components";
import { useTranslation } from "react-i18next";
import { usePatchTerminalState } from "../../../../external/sources/terminal/terminal.queries";
import { useButtonStateFilter } from "../hooks/useButtonFilter";
import { State } from "@mimirorg/typelibrary-types";
import { PlainLink } from "../../../common/plain-link";
import { Button } from "../../../../complib/buttons";
import { ChevronUp, DocumentDuplicate, PencilSquare, Trash } from "@styled-icons/heroicons-outline";
import { AlertDialog } from "../../../../complib/overlays";
import { UserItem } from "../../../../common/types/userItem";
import { TerminalItem } from "../../../../common/types/terminalItem";
import { AspectObjectItem } from "../../../../common/types/aspectObjectItem";
import { AttributeItem } from "../../../../common/types/attributeItem";
import { getCloneLink, getEditLink } from "./SearchItemActions.helpers";

type SearchItemProps = {
  user?: UserItem;
  item: TerminalItem | AspectObjectItem | AttributeItem;
  children?: React.ReactNode;
};

export const SearchItemActions = ({ user, item, children }: SearchItemProps) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");
  const patchMutation = usePatchTerminalState();
  const btnFilter = useButtonStateFilter(item, user);

  const deleteAction = {
    name: t("search.item.delete"),
    onAction: () => patchMutation.mutate({ id: item.id, state: State.Delete }),
  };

  const approveAction = {
    name: t("search.item.approve"),
    onAction: () => patchMutation.mutate({ id: item.id, state: State.Approve }),
  };

  const cloneLink = btnFilter.clone ? getCloneLink(item) : "#";
  const editLink = btnFilter.edit ? getEditLink(item) : "#";

  return (
    <>
      <PlainLink tabIndex={-1} to={cloneLink}>
        <Button
          disabled={!btnFilter.clone}
          tabIndex={0}
          as={!btnFilter.clone ? "button" : "span"}
          icon={<DocumentDuplicate />}
          iconOnly
        >
          {t("search.item.clone")}
        </Button>
      </PlainLink>
      <PlainLink tabIndex={-1} to={editLink}>
        <Button
          disabled={!btnFilter.edit}
          tabIndex={0}
          as={!btnFilter.edit ? "button" : "span"}
          icon={<PencilSquare />}
          iconOnly
        >
          {t("search.item.edit")}
        </Button>
      </PlainLink>
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[deleteAction]}
        title={t("search.item.templates.delete", { object: name })}
        description={t("search.item.deleteDescription")}
        hideDescription
        content={children}
      >
        <Button
          disabled={!btnFilter.delete}
          variant={btnFilter.deleted ? "outlined" : "filled"}
          icon={<Trash />}
          iconOnly
        >
          {t("search.item.delete")}
        </Button>
      </AlertDialog>

      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[approveAction]}
        title={t("search.item.templates.approve")}
        description={t("search.item.approveDescription")}
        hideDescription
        content={children}
      >
        <Button
          disabled={!btnFilter.approve}
          variant={btnFilter.approved ? "outlined" : "filled"}
          icon={<ChevronUp />}
          iconOnly
        >
          {t("search.item.approve")}
        </Button>
      </AlertDialog>
    </>
  );
};
