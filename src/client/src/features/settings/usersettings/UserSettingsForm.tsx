import { useServerValidation } from "hooks/server-validation/useServerValidation";
//import { yupResolver } from "@hookform/resolvers/yup";
import { MimirorgUserAm, MimirorgUserCm } from "@mimirorg/typelibrary-types";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
//import { userSchema } from "features/settings/usersettings/userSchema";
import {
  addDummyPasswordToUserAm,
  mapMimirorgUserCmToAm,
  useUpdatingToast,
  useUserQuery,
} from "features/settings/usersettings/userSettingsForm.helpers";
import { useUpdateUser } from "api/user.queries";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import { onSubmitForm, usePrefilledForm } from "helpers/form.helpers";
import { DevTool } from "@hookform/devtools";
import { Loader } from "components/Loader";
import { Button, Form, FormField, Input } from "@mimirorg/component-library";

interface UserSettingsFormProps {
  defaultValues?: MimirorgUserAm;
}

export const UserSettingsForm = ({ defaultValues }: UserSettingsFormProps) => {
  const { t } = useTranslation("settings");

  const formMethods = useForm<MimirorgUserAm>({
    defaultValues: defaultValues,
    //resolver: yupResolver(userSchema(t)),
  });

  const { register, handleSubmit, control, setError, reset, formState } = formMethods;

  const query = useUserQuery();
  const mapper = (userCm: MimirorgUserCm) => mapMimirorgUserCmToAm(userCm);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useUpdateUser();
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useUpdatingToast();

  return (
    <Form onSubmit={handleSubmit((data) => onSubmitForm(addDummyPasswordToUserAm(data), mutation.mutateAsync, toast))}>
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <FormField label={t("usersettings.labels.firstName")} error={formState.errors.firstName}>
            <Input
              style={{ fontSize: "17px" }}
              placeholder={t("usersettings.placeholders.firstName")}
              {...register("firstName")}
            />
          </FormField>
          <FormField label={t("usersettings.labels.lastName")} error={formState.errors.lastName}>
            <Input
              style={{ fontSize: "17px" }}
              placeholder={t("usersettings.placeholders.lastName")}
              {...register("lastName")}
            />
          </FormField>
          <Button type={"submit"}>{t("usersettings.submit")}</Button>
        </>
      )}
      <DevTool control={control} placement={"bottom-right"} />
    </Form>
  );
};
