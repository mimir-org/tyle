import { DevTool } from "@hookform/devtools";
import { MimirorgQrCodeCm, MimirorgVerifyAm } from "@mimirorg/typelibrary-types";
import { useExecuteOnCriteria } from "common/hooks/useExecuteOnCriteria";
import { Button } from "complib/buttons";
import { Digits } from "complib/inputs";
import { Flexbox, Input, Text } from "@mimirorg/component-library";
import { Actionable } from "complib/types";
import { useGenerateMfa } from "external/sources/user/user.queries";
import { AuthContent } from "features/auth/common/auth-content/AuthContent";
import { Error } from "features/auth/common/error/Error";
import { Processing } from "features/auth/common/processing/Processing";
import { MotionVerifyForm } from "features/auth/common/verification/Verification";
import { onSubmitForm } from "features/auth/recover/verification/RecoverVerification.helpers";
import { Controller, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

type VerificationProps = Pick<MimirorgVerifyAm, "email"> & {
  setMfaInfo: (info: MimirorgQrCodeCm) => void;
  setVerificationInfo?: (info: MimirorgVerifyAm) => void;
  cancel?: Partial<Actionable>;
  complete?: Partial<Actionable>;
};

export const RecoverVerification = ({
  email,
  setMfaInfo,
  setVerificationInfo,
  cancel,
  complete,
}: VerificationProps) => {
  const theme = useTheme();
  const { t } = useTranslation("auth");
  const { control, register, handleSubmit } = useForm<MimirorgVerifyAm>();

  const generateMfaMutation = useGenerateMfa();

  const showProcessing = generateMfaMutation.isLoading;
  const showError = generateMfaMutation.isError;
  const showInput = !showProcessing;

  useExecuteOnCriteria(complete?.onAction, generateMfaMutation.isSuccess);

  return (
    <AuthContent
      title={t("recover.verify.title")}
      firstRow={
        <>
          {showProcessing && <Processing>{t("recover.processing")}</Processing>}
          {showError && <Error>{t("recover.verify.error")}</Error>}
          {showInput && (
            <MotionVerifyForm
              id={"verify-form"}
              onSubmit={handleSubmit((data) =>
                onSubmitForm(data, generateMfaMutation.mutateAsync, setMfaInfo, setVerificationInfo),
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
          <Text textAlign={"center"}>{t("recover.verify.info.text")}</Text>
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
