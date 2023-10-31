//import { yupResolver } from "@hookform/resolvers/yup";
import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { UserItem } from "common/types/userItem";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { Button, Form, FormField, Input, Select } from "@mimirorg/component-library";
import { usePermissionToasts } from "components/PermissionCard/PermissionCardForm.helpers";
//import { permissionSchema } from "features/settings/common/permission-card/card-form/permissionSchema";
import { FormUserPermission } from "components/PermissionCard/formUserPermission";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";

export interface PermissionCardFormProps {
  user: UserItem;
  formId?: string;
  onSubmit?: () => void;
  showSubmitButton?: boolean;
}

export const PermissionCardForm = ({ user, formId, onSubmit, showSubmitButton = true }: PermissionCardFormProps) => {
  const { t } = useTranslation(["settings"]);

  const permissionOptions = getOptionsFromEnum<MimirorgPermission>(MimirorgPermission);
  const currentPermission = permissionOptions.find((x) => x.value === user.permissions[user.company.id]?.value);

  const { register, control, handleSubmit, formState } = useForm<FormUserPermission>({
    /*resolver: yupResolver(permissionSchema(t)),
    defaultValues: {
      userId: user.id,
      companyId: user.company?.id,
      permission: currentPermission ?? permissionOptions[0],
    },*/
  });

  const toast = usePermissionToasts(currentPermission);

  return (
    <Form
      id={formId}
      alignItems={"center"}
      onSubmit={handleSubmit((data) => toast(user.name, data).then(() => onSubmit && onSubmit()))}
    >
      <Input type={"hidden"} value={user.id} {...register("userId")} />
      <Input type={"hidden"} value={user.company?.id} {...register("companyId")} />
      <Controller
        control={control}
        name={"permission"}
        render={({ field: { value, ref, ...rest } }) => (
          <FormField label={t("common.permission.permission")} error={formState.errors.permission} indent={false}>
            <Select
              {...rest}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("common.permission.permission").toLowerCase() })}
              options={permissionOptions}
              value={permissionOptions.find((x) => x.value === value.value)}
            />
          </FormField>
        )}
      />
      {showSubmitButton && <Button type={"submit"}>{t("common.permission.submit")}</Button>}
    </Form>
  );
};
