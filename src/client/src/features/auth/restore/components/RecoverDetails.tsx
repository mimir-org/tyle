import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { Button } from "../../../../complib/buttons";
import { Form, FormField } from "../../../../complib/form";
import { Input } from "../../../../complib/inputs";
import { Text } from "../../../../complib/text";
import { Actionable } from "../../../../complib/types";
import { useGenerateChangePasswordSecret } from "../../../../data/queries/auth/queriesUser";
import { useExecuteOnCriteria } from "../../../../hooks/useExecuteOnCriteria";
import { useServerValidation } from "../../../../hooks/server-validation/useServerValidation";
import { UnauthenticatedContent } from "../../../ui/unauthenticated/layout/UnauthenticatedContent";
import { Error } from "../../common/Error";
import { Processing } from "../../common/Processing";
import { recoverDetailsSchema } from "./recoverDetailsSchema";

interface RecoverDetailsProps {
  setUserEmail: (email: string) => void;
  complete?: Partial<Actionable>;
}

type RecoverModel = { email: string };

export const RecoverDetails = ({ complete, setUserEmail }: RecoverDetailsProps) => {
  const { t } = useTranslation();

  const formMethods = useForm<RecoverModel>({
    resolver: yupResolver(recoverDetailsSchema(t)),
  });

  const { register, control, handleSubmit, setError, formState } = formMethods;
  const { errors } = formState;

  const mutation = useGenerateChangePasswordSecret();

  useServerValidation(mutation.error, setError);
  useExecuteOnCriteria(complete?.onAction, mutation.isSuccess);

  return (
    <UnauthenticatedContent
      title={t("recover.details.title")}
      firstRow={
        <>
          {mutation.isLoading && <Processing>{t("recover.processing")}</Processing>}
          {!mutation.isSuccess && !mutation.isLoading && (
            <Form
              id={"details-form"}
              onSubmit={handleSubmit((data) => {
                setUserEmail(data.email);
                mutation.mutate(data.email);
              })}
            >
              {mutation.isError && <Error>{t("recover.details.error")}</Error>}
              <FormField label={`${t("common.fields.email")} *`} error={errors.email}>
                <Input id="email" type="email" placeholder={t("common.placeholders.email")} {...register("email")} />
              </FormField>
            </Form>
          )}
          <DevTool control={control} placement={"bottom-right"} />
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>{t("recover.details.info.text")}</Text>
          <Button type={"submit"} form={"details-form"} alignSelf={"center"}>
            {complete?.actionText}
          </Button>
        </>
      }
    />
  );
};
