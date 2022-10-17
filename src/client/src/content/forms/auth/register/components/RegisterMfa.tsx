import { MimirorgQrCodeCm } from "@mimirorg/typelibrary-types";
import { useTheme } from "styled-components";
import { Button } from "../../../../../complib/buttons";
import { Flexbox } from "../../../../../complib/layouts";
import { Icon } from "../../../../../complib/media";
import { Text } from "../../../../../complib/text";
import { Actionable } from "../../../../../complib/types";
import {
  UnauthenticatedContent,
  UnauthenticatedContentProps,
} from "../../../../app/components/unauthenticated/layout/UnauthenticatedContent";

type RegisterMfaProps = Pick<UnauthenticatedContentProps, "title" | "infoTitle" | "infoText"> &
  MimirorgQrCodeCm & {
    codeTitle?: string;
    cancel?: Partial<Actionable>;
    complete?: Partial<Actionable>;
  };

export const RegisterMfa = (props: RegisterMfaProps) => {
  const theme = useTheme();
  const { title, infoTitle, infoText } = props;
  const { codeTitle, code, manualCode } = props;
  const { cancel, complete } = props;

  const showQrDialog = code && manualCode;

  return (
    <UnauthenticatedContent title={title} infoTitle={infoTitle} infoText={infoText}>
      {showQrDialog && (
        <Flexbox flex={1} flexDirection={"column"} justifyContent={"space-evenly"}>
          <Flexbox as={"section"} flexDirection={"column"} alignItems={"center"} gap={theme.tyle.spacing.base}>
            <Text variant={"headline-small"}>{codeTitle}</Text>
            <Icon size={180} src={code} alt="" />
          </Flexbox>

          <Flexbox gap={theme.tyle.spacing.xxl} alignSelf={"center"}>
            {cancel?.actionable && (
              <Button variant={"outlined"} onClick={cancel.onAction}>
                {cancel.actionText}
              </Button>
            )}
            {complete?.actionable && (
              <Button type={"submit"} onClick={complete.onAction}>
                {complete.actionText}
              </Button>
            )}
          </Flexbox>
        </Flexbox>
      )}
    </UnauthenticatedContent>
  );
};
