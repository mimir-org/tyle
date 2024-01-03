import { AlertDialog } from "@mimirorg/component-library";
import { Check, DocumentDuplicate, PencilSquare, Trash } from "@styled-icons/heroicons-outline";
import { AxiosError } from "axios";
import Button from "components/Button";
import PlainLink from "components/PlainLink";
import StateBadge from "components/StateBadge";
import Text from "components/Text";
import { toast } from "components/Toaster/toast";
import Tooltip from "components/Tooltip";
import { useState } from "react";
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
  const btnFilter = useButtonStateFilter(item, user);

  const patchMutation = usePatchMutation(item);
  const deleteMutation = useDeleteMutation(item);

  const submitToast = (submissionPromise: Promise<unknown>) =>
    toast.promise(submissionPromise, {
      loading: "Sending request",
      success: "Request submitted",
      error: (error: AxiosError) => {
        if (error.response?.status === 403) return `403 (Forbidden) error: ${error.response?.data ?? ""}`;
        return "An error occurred while sending the request";
      },
    });

  const deleteAction = {
    name: "Delete",
    onAction: () => {
      const mutation = deleteMutation.mutateAsync();
      submitToast(mutation);
    },
  };

  const approveAction = {
    name: "Send approval request",
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
        <Tooltip content={<Text>Clone</Text>}>
          <Button
            disabled={!btnFilter.clone}
            tabIndex={0}
            as={!btnFilter.clone ? "button" : "span"}
            icon={<DocumentDuplicate />}
            iconOnly
          >
            Clone
          </Button>
        </Tooltip>
      </PlainLink>

      <PlainLink tabIndex={-1} to={editLink}>
        <Tooltip content={<Text>Edit</Text>}>
          <Button
            disabled={!btnFilter.edit}
            tabIndex={0}
            as={!btnFilter.edit ? "button" : "span"}
            icon={<PencilSquare />}
            iconOnly
          >
            Edit
          </Button>
        </Tooltip>
      </PlainLink>

      {!isAttributeGroup && (
        <>
          <AlertDialog
            gap={theme.tyle.spacing.multiple(6)}
            actions={[approveAction]}
            title="Do you want to send approval request?"
            description="Someone has to accept this scope change."
            hideDescription
            content={children}
            open={isApprovalOpen}
            onOpenChange={(open) => setIsApprovalOpen(open)}
          />

          <Tooltip content={<Text>Send approval request</Text>}>
            <Button
              disabled={!btnFilter.review}
              tabIndex={0}
              variant={btnFilter.approved ? "outlined" : "filled"}
              icon={<Check />}
              iconOnly
              onClick={() => setIsApprovalOpen(true)}
            >
              Send approval request
            </Button>
          </Tooltip>
        </>
      )}
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[deleteAction]}
        title="Do you want to delete this item?"
        description="The item will be deleted."
        hideDescription
        content={children}
        open={isDeleteOpen}
        onOpenChange={(open) => setIsDeleteOpen(open)}
      />
      <Tooltip content={<Text>Delete</Text>}>
        <Button
          disabled={!btnFilter.delete}
          variant={"filled"}
          icon={<Trash />}
          dangerousAction
          iconOnly
          onClick={() => setIsDeleteOpen(true)}
        >
          Delete
        </Button>
      </Tooltip>
    </>
  );
};

export default SearchItemActions;
