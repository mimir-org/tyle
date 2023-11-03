import { AlertDialog, Button, Text, Tooltip, toast } from "@mimirorg/component-library";
import { Check, DocumentDuplicate, PencilSquare, Trash } from "@styled-icons/heroicons-outline";
import { AxiosError } from "axios";
import PlainLink from "components/PlainLink";
import StateBadge from "components/StateBadge";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { State } from "types/common/state";
import { ItemType } from "types/itemTypes";
import { UserItem } from "types/userItem";
import { getCloneLink, getEditLink, useDeleteMutation, usePatchMutation } from "./SearchItemActions.helpers";
import { useButtonStateFilter } from "./useButtonFilter";

type SearchItemProps = {
  user: UserItem | null;
  item: ItemType;
  children?: React.ReactNode;
  isAttributeGroup?: boolean;
};

const SearchItemActions = ({ user, item, children, isAttributeGroup = false }: SearchItemProps) => {
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
      const mutation = patchMutation.mutateAsync({ state: State.Review });
      submitToast(mutation);
    },
  };

  const cloneLink = btnFilter.clone ? getCloneLink(item) : "#";
  const editLink = btnFilter.edit ? getEditLink(item) : "#";
  const isStateApproved = item.state === State.Approved;

  return (
    <>
      {!isStateApproved && !isAttributeGroup && <StateBadge state={item.state} />}

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

      {!isAttributeGroup && (
        <>
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
        </>
      )}
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

export default SearchItemActions;
