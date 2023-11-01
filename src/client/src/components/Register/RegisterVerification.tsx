import { DevTool } from "@hookform/devtools";
import { MimirorgQrCodeCm, MimirorgVerifyAm } from "@mimirorg/typelibrary-types";
import { useExecuteOnCriteria } from "hooks/useExecuteOnCriteria";
import { Actionable, Button, Flexbox, Input, Text } from "@mimirorg/component-library";
import { useGenerateMfa, useVerification } from "api/user.queries";
import { AuthContent } from "components/AuthContent/AuthContent";
import { Error } from "components/Error/Error";
import { Processing } from "components/Processing/Processing";
import { MotionVerifyForm } from "components/MotionVerifyForm/MotionVerifyForm";
import { onSubmitForm } from "components/Register/RegisterVerification.helpers";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Digits } from "components/Digits/Digits";

type VerificationProps = Pick<MimirorgVerifyAm, "email"> & {
  setMfaInfo: (info: MimirorgQrCodeCm) => void;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
};

export const RegisterVerification = ({ email, setMfaInfo, cancel, complete }: VerificationProps) => {
  const theme = useTheme();
  const { t } = useTranslation("auth");
  const { control, register, handleSubmit } = useForm<MimirorgVerifyAm>();

  const generateMfaMutation = useGenerateMfa();
  const verificationMutation = useVerification();

  const showError = generateMfaMutation.isError || verificationMutation.isError;
  const showProcessing = verificationMutation.isLoading || generateMfaMutation.isLoading;
  const showInput = !verificationMutation.isSuccess && !verificationMutation.isLoading;

  useExecuteOnCriteria(complete?.onAction, verificationMutation.isSuccess && generateMfaMutation.isSuccess);

  return (
    <AuthContent
      title={t("register.verify.title")}
      firstRow={
        <>
          {showProcessing && <Processing>{t("register.processing")}</Processing>}
          {showError && <Error>{t("register.verify.error")}</Error>}
          {showInput && (
            <MotionVerifyForm
              id={"verify-form"}
              onSubmit={handleSubmit((data) =>
                onSubmitForm(data, verificationMutation.mutateAsync, generateMfaMutation.mutateAsync, setMfaInfo),
              )}
              layout
            >
              <Input type={"hidden"} value={email} {...register("email")} />
              <Controller
                control={control}
                name={"code"}
                render={({ field: { value, onChange } }) => <Digits value={value} onChange={onChange} />}
              />
            </MotionVerifyForm>
          )}
          <DevTool control={control} placement={"bottom-right"} />
        </>
      }
      secondRow={
        <>
          <Text textAlign={"center"}>{t("register.verify.info.text")}</Text>
          <Flexbox gap={theme.mimirorg.spacing.xxl} alignSelf={"center"}>
            {cancel?.actionable && (
              <Button variant={"outlined"} onClick={cancel.onAction}>
                {cancel.actionText}
              </Button>
            )}
            {complete?.actionable && (
              <Button type={"submit"} form={"verify-form"}>
                {complete.actionText}
              </Button>
            )}
          </Flexbox>
        </>
      }
    />
  );
};
