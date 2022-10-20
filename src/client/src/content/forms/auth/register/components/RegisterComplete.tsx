import { useTheme } from "styled-components";
import { Button } from "../../../../../complib/buttons";
import { Box } from "../../../../../complib/layouts";
import { Text } from "../../../../../complib/text";
import { Actionable } from "../../../../../complib/types";
import { UnauthenticatedContent } from "../../../../app/components/unauthenticated/layout/UnauthenticatedContent";

type RegisterCompleteProps = Partial<Actionable> & {
  title: string;
  infoText: string;
};

export const RegisterComplete = ({ title, infoText, actionable, actionText, onAction }: RegisterCompleteProps) => {
  const theme = useTheme();

  return (
    <UnauthenticatedContent
      title={title}
      firstRow={
        <Box
          display={"flex"}
          flexDirection={"column"}
          alignItems={"center"}
          alignSelf={"center"}
          gap={theme.tyle.spacing.xxxl}
          maxWidth={"300px"}
        >
          <Text textAlign={"center"}>{infoText}</Text>
          {actionable && <Button onClick={onAction}>{actionText}</Button>}
        </Box>
      }
    />
  );
};
