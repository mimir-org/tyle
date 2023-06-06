import { useTheme } from "styled-components";
import { useTranslation } from "react-i18next";
import { usePatchTerminalState } from "../../../../external/sources/terminal/terminal.queries";
import { useButtonStateFilter } from "../hooks/useButtonFilter";
import { State } from "@mimirorg/typelibrary-types";
import { PlainLink } from "../../../common/plain-link";
import { Button } from "../../../../complib/buttons";
import { Check, DocumentDuplicate, PencilSquare, Trash } from "@styled-icons/heroicons-outline";
import { AlertDialog } from "../../../../complib/overlays";
import { UserItem } from "../../../../common/types/userItem";
import { getCloneLink, getEditLink } from "./SearchItemActions.helpers";
import { ItemType } from "../../../entities/types/itemTypes";
import { usePatchRdsState } from "../../../../external/sources/rds/rds.queries";
import { usePatchAspectObjectState } from "../../../../external/sources/aspectobject/aspectObject.queries";
import { usePatchAttributeState } from "../../../../external/sources/attribute/attribute.queries";
import { usePatchUnitState } from "../../../../external/sources/unit/unit.queries";
import { usePatchQuantityDatumState } from "../../../../external/sources/datum/quantityDatum.queries";
import { toast } from "complib/data-display";
import { AxiosError } from "axios";

type SearchItemProps = {
  user: UserItem | null;
  item: ItemType;
  children?: React.ReactNode;
};

export const SearchItemActions = ({ user, item, children }: SearchItemProps) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");
  const patchAspectObjectMutation = usePatchAspectObjectState();
  const patchTerminalMutation = usePatchTerminalState();
  const patchUnitMutation = usePatchUnitState();
  const patchQuantityDatumMutation = usePatchQuantityDatumState();
  const patchRdsMutation = usePatchRdsState();
  const patchAttributeMutation = usePatchAttributeState();
  const btnFilter = useButtonStateFilter(item, user);

  function getMutation() {
    switch (item.kind) {
      case "AspectObjectItem":
        return patchAspectObjectMutation;
      case "TerminalItem":
        return patchTerminalMutation;
      case "AttributeItem":
        return patchAttributeMutation;
      case "UnitItem":
        return patchUnitMutation;
      case "QuantityDatumItem":
        return patchQuantityDatumMutation;
      case "RdsItem":
        return patchRdsMutation;
      default:
        throw new Error("Unknown item kind");
    }
  }

  const submitToast = (submissionPromise: Promise<unknown>) =>
    toast.promise(submissionPromise, {
      loading: t("search.item.toast.loading"),
      success: t("search.item.toast.success"),
      error: (error: AxiosError) => {
        if (error.response?.status === 403)
          return t("search.item.toast.error.403", { data: error.response?.data ?? "" });
        return t("search.item.toast.error.default");
      },
    });

  const deleteAction = {
    name: t("search.item.delete"),
    onAction: () => {
      const mutation = getMutation().mutateAsync({ id: item.id, state: State.Delete });
      submitToast(mutation);
    },
  };

  const approveAction = {
    name: t("search.item.approve"),
    onAction: () => {
      const mutation = getMutation().mutateAsync({ id: item.id, state: State.Approve });
      submitToast(mutation);
    },
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
        actions={[approveAction]}
        title={t("search.item.templates.approve")}
        description={t("search.item.approveDescription")}
        hideDescription
        content={children}
      >
        <Button
          disabled={!btnFilter.approve}
          variant={btnFilter.approved ? "outlined" : "filled"}
          icon={<Check />}
          iconOnly
        >
          {t("search.item.approve")}
        </Button>
      </AlertDialog>
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
          dangerousAction
          iconOnly
        >
          {t("search.item.delete")}
        </Button>
      </AlertDialog>
    </>
  );
};
