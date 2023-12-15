import { Button, Form, FormField, Input, Select } from "@mimirorg/component-library";
import { UserItem } from "types/userItem";
import { getOptionsFromEnum } from "../../utils";
import { Role } from "../../types/Role";

export interface RoleCardFormProps {
  user: UserItem;
  formId?: string;
  showSubmitButton?: boolean;
  selectedRole: string;
  setSelectedRole: (role: string) => void;
}
const RoleCardForm = ({
  user,
  formId,
  showSubmitButton = true,
  setSelectedRole,
  selectedRole,
}: RoleCardFormProps) => {
  const roleOptions = getOptionsFromEnum<Role>(Role);

  return (
    <Form id={formId} alignItems={"center"}>
      <Input type={"hidden"} value={user.id} />
      <FormField label="Roles" indent={false}>
        <Select
          options={roleOptions}
          value={roleOptions?.find((x) => x.label === selectedRole)}
          onChange={(x) => {
            setSelectedRole(x!.label);
          }}
        />
      </FormField>
      {showSubmitButton && <Button type={"submit"}>Submit</Button>}
    </Form>
  );
};

export default RoleCardForm;
