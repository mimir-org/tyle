import { PencilSquare } from "@styled-icons/heroicons-outline";
import AlertDialog, { AlertDialogActionItem, AlertDialogCancelItem } from "components/AlertDialog";
import Button from "components/Button";
import RoleCard from "components/RoleCard";
import { useState } from "react";
import { UserItem } from "types/userItem";

interface RoleDialogProps {
  user: UserItem;
  handleRoleChange: (user: UserItem, newRole: string | undefined) => void;
}

/**
 * Dialog that presents the user with a form to change a user's permission level
 *
 * The dialog's open state is overridden to allow for form submission withing the context of the dialog.
 * The dialog's action is extended with the submit type and a form id so that it can submit an external component form.
 *
 * @param user
 * @param handleRoleChange
 * @constructor
 */
const RoleDialog = ({ user, handleRoleChange }: RoleDialogProps) => {
  const formId = "changeUserPermission";
  const [open, setOpen] = useState(false);
  const [selectedRole, setSelectedRole] = useState<string>(user.roles[0]);

  const dialogContent = (
    <RoleCard
      selected
      user={user}
      formId={formId}
      showSubmitButton={false}
      selectedRole={selectedRole}
      setSelectedRole={setSelectedRole}
    />
  );
  const dialogOverriddenSubmitAction: AlertDialogActionItem = {
    name: "Submit",
    form: formId,
    type: "submit",
    onAction: () => {
      handleRoleChange(user, selectedRole);
      setOpen(false);
    },
  };
  const dialogOverriddenCancelAction: AlertDialogCancelItem = {
    name: "Cancel",
    onAction: () => {
      setOpen(false);
    },
  };

  return (
    <AlertDialog
      open={open}
      title="Editing"
      description="Change the user's role"
      content={dialogContent}
      actions={[dialogOverriddenSubmitAction]}
      cancelAction={dialogOverriddenCancelAction}
    >
      <Button
        variant={"text"}
        icon={<PencilSquare />}
        iconOnly
        onClick={() => {
          setOpen(true);
        }}
      >
        {user.name}
      </Button>
    </AlertDialog>
  );
};

export default RoleDialog;
