import { useTheme } from "styled-components";
import { useTranslation } from "react-i18next";
import { useButtonStateFilter } from "../hooks/useButtonFilter";
import { State } from "@mimirorg/typelibrary-types";
import { PlainLink } from "../../../common/plain-link";
import { Check, DocumentDuplicate, PencilSquare, Trash } from "@styled-icons/heroicons-outline";
import { AlertDialog } from "../../../../complib/overlays";
import { UserItem } from "../../../../common/types/userItem";
import { getCloneLink, getEditLink, useDeleteMutation, usePatchMutation } from "./SearchItemActions.helpers";
import { ItemType } from "../../../entities/types/itemTypes";
import { Button, Text, Tooltip } from "@mimirorg/component-library";
import { StateBadge } from "../../../ui/badges/StateBadge";
import { toast } from "complib/data-display";
import { AxiosError } from "axios";
import { useState } from "react";

type SearchItemProps = {
  user: UserItem | null;
  item: ItemType;
  children?: React.ReactNode;
};

export const SearchItemActions = ({ user, item, children }: SearchItemProps) => {
  const [isApprovalOpen, setIsApprovalOpen] = useState(false);
  const [isDeleteOpen, setIsDeleteOpen] = useState(false);
  const theme = useTheme();
  const { t } = useTranslation("explore");
  const btnFilter = useButtonStateFilter(item, user);

  const patchMutation = usePatchMutation(item);
  const deleteMutation = useDeleteMutation(item);

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
      const mutation = deleteMutation.mutateAsync();
      submitToast(mutation);
    },
  };

  const approveAction = {
    name: t("search.item.approve"),
    onAction: () => {
      const mutation = patchMutation.mutateAsync({ id: item.id, state: State.Review });
      submitToast(mutation);
    },
  };

  const cloneLink = btnFilter.clone ? getCloneLink(item) : "#";
  const editLink = btnFilter.edit ? getEditLink(item) : "#";
  const isStateApproved = item.state === State.Approved;

  return (
    <>
      {!isStateApproved && <StateBadge state={item.state} />}
      <PlainLink tabIndex={-1} to={cloneLink}>
        <Tooltip content={<Text>{t("search.item.clone")}</Text>}>
          <Button
            disabled={!btnFilter.clone}
            tabIndex={0}
            as={!btnFilter.clone ? "button" : "span"}
            icon={<DocumentDuplicate />}
            iconOnly
          >
            {t("search.item.clone")}
          </Button>
        </Tooltip>
      </PlainLink>
      <PlainLink tabIndex={-1} to={editLink}>
        <Tooltip content={<Text>{t("search.item.edit")}</Text>}>
          <Button
            disabled={!btnFilter.edit}
            tabIndex={0}
            as={!btnFilter.edit ? "button" : "span"}
            icon={<PencilSquare />}
            iconOnly
          >
            {t("search.item.edit")}
          </Button>
        </Tooltip>
      </PlainLink>
      <AlertDialog
        gap={theme.mimirorg.spacing.multiple(6)}
        actions={[approveAction]}
        title={t("search.item.templates.approve")}
        description={t("search.item.approveDescription")}
        hideDescription
        content={children}
        open={isApprovalOpen}
        onOpenChange={(open) => setIsApprovalOpen(open)}
      />
      <Tooltip content={<Text>{t("search.item.approve")}</Text>}>
        <Button
          disabled={!btnFilter.review}
          tabIndex={0}
          variant={btnFilter.approved ? "outlined" : "filled"}
          icon={<Check />}
          iconOnly
          onClick={() => setIsApprovalOpen(true)}
        >
          {t("search.item.approve")}
        </Button>
      </Tooltip>
      <AlertDialog
        gap={theme.mimirorg.spacing.multiple(6)}
        actions={[deleteAction]}
        title={t("search.item.templates.delete", { object: name })}
        description={t("search.item.deleteDescription")}
        hideDescription
        content={children}
        open={isDeleteOpen}
        onOpenChange={(open) => setIsDeleteOpen(open)}
      />
      <Tooltip content={<Text>{t("search.item.delete")}</Text>}>
        <Button
          disabled={!btnFilter.delete}
          variant={"filled"}
          icon={<Trash />}
          dangerousAction
          iconOnly
          onClick={() => setIsDeleteOpen(true)}
        >
          {t("search.item.delete")}
        </Button>
      </Tooltip>
    </>
  );
};
