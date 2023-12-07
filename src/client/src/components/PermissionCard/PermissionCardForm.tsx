import { Button, Form, FormField, Input, Select } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";
import { UserItem } from "types/userItem";
import { getOptionsFromEnum } from "../../utils";
import { Role } from "../../types/Role";
import { useState } from "react";
import { useAddUserToRole } from "../../api/authorize.queries";

export interface PermissionCardFormProps {
  user: UserItem;
  formId?: string;
  onSubmit?: () => void;
  showSubmitButton?: boolean;
}

const PermissionCardForm = ({ user, formId, showSubmitButton = true }: PermissionCardFormProps) => {
  //TODO Remove translation
  const { t } = useTranslation(["settings"]);
  const [ currentRole, setRole ] = useState<string | undefined>(user.roles[0]);

  const roleOptions = getOptionsFromEnum<Role>(Role);

  //const toast = usePermissionToasts(currentPermission);

  return (
    <Form
      id={formId}
      alignItems={"center"}
      onSubmit={(data) => {
        console.log(data);
      }}
    >
      <Input type={"hidden"} value={user.id} />

          <FormField label={t("common.permission.permission")} indent={false}>
            <Select
              placeholder={t("common.templates.select", { object: t("common.permission.permission").toLowerCase() })}
              options={roleOptions}
              value={roleOptions?.find((x) => x.label === currentRole)}
              onChange={(x) => {setRole(x?.label)}}
            />
          </FormField>
      {showSubmitButton && <Button type={"submit"}>{t("common.permission.submit")}</Button>}
    </Form>
  );
};

export default PermissionCardForm;
