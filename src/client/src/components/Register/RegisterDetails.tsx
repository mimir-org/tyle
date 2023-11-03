import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  Actionable,
  Button,
  Form,
  FormField,
  FormFieldset,
  Input,
  MotionFlexbox,
  MotionText,
  Text,
  Textarea,
} from "@mimirorg/component-library";
import { useCreateUser } from "api/user.queries";
import AuthContent from "components/AuthContent";
import Error from "components/Error";
import Processing from "components/Processing";
import { useExecuteOnCriteria } from "hooks/useExecuteOnCriteria";
import { useServerValidation } from "hooks/useServerValidation";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import { useTheme } from "styled-components";
import { MimirorgUserAmCorrectTypes } from "./mimirorgUserAm";
import { registerDetailsSchema } from "./registerDetailsSchema";

interface RegisterDetailsProps {
  setUserEmail: (email: string) => void;
  complete?: Partial<Actionable>;
}

const RegisterDetails = ({ complete, setUserEmail }: RegisterDetailsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("auth");

  const mutation = useCreateUser();

  const formMethods = useForm({
    resolver: yupResolver(registerDetailsSchema(t)),
  });

  const { register, control, handleSubmit, setError, formState } = formMethods;
  const { errors } = formState;

  const onSubmit = (data: MimirorgUserAmCorrectTypes) => {
    setUserEmail(data.email);
    mutation.mutate({
      ...data,
      purpose: data.purpose ?? "",
    });
  };

  useServerValidation(mutation.error, setError);
  useExecuteOnCriteria(complete?.onAction, mutation.isSuccess);

  return (
    <AuthContent
      title={t("register.details.title")}
      subtitle={t("register.details.description")}
      firstRow={
        <>
          {mutation.isLoading && <Processing>{t("register.processing")}</Processing>}
          {!mutation.isSuccess && !mutation.isLoading && (
            <Form id={"details-form"} onSubmit={handleSubmit((data) => onSubmit(data))}>
              {mutation.isError && <Error>{t("register.details.error")}</Error>}
              <FormFieldset>
                <FormField label={`${t("register.details.email")} *`} error={formState.errors.email}>
                  <Input
                    id="email"
                    type="email"
                    placeholder={t("register.details.placeholders.email")}
                    {...register("email")}
                  />
                </FormField>

                <FormField label={`${t("register.details.firstname")} *`} error={errors.firstName}>
                  <Input
                    id="firstName"
                    placeholder={t("register.details.placeholders.firstname")}
                    {...register("firstName")}
                  />
                </FormField>

                <FormField label={`${t("register.details.lastname")} *`} error={errors.lastName}>
                  <Input
                    id="lastName"
                    placeholder={t("register.details.placeholders.lastname")}
                    {...register("lastName")}
                  />
                </FormField>

                <FormField label={`${t("register.details.password")} *`} error={errors.password}>
                  <Input
                    id="password"
                    type="password"
                    placeholder={t("register.details.placeholders.password")}
                    {...register("password")}
                  />
                </FormField>

                <FormField label={`${t("register.details.confirmPassword")} *`} error={errors.confirmPassword}>
                  <Input
                    id="confirmPassword"
                    type="password"
                    placeholder={t("register.details.placeholders.password")}
                    {...register("confirmPassword")}
                  />
                </FormField>

                <FormField label={`${t("register.details.purpose")} *`} error={errors.purpose}>
                  <Textarea placeholder={t("register.details.placeholders.purpose")} {...register("purpose")} />
                </FormField>

                <MotionText color={theme.mimirorg.color.surface.variant.on} layout={"position"} as={"i"}>
                  {t("register.details.placeholders.required")}
                </MotionText>
              </FormFieldset>
            </Form>
          )}
          <DevTool control={control} placement={"bottom-right"} />
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>{t("register.details.info.text")}</Text>
          <MotionFlexbox layout flexDirection={"column"} alignItems={"center"} gap={theme.mimirorg.spacing.xxl}>
            <Button type={"submit"} form={"details-form"}>
              {complete?.actionText}
            </Button>
            <Text color={theme.mimirorg.color.surface.variant.on}>
              {t("register.details.altLead")} <Link to="/">{t("register.details.altLink")}</Link>
            </Text>
          </MotionFlexbox>
        </>
      }
    />
  );
};

export default RegisterDetails;