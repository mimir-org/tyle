import { Flex } from "../../../../compLibrary/layout/Flex";
import { Spinner } from "../../../../compLibrary/spinner";
import { TextResources } from "../../../../assets/text";
import { THEME } from "../../../../compLibrary/core/constants";

export const RegisterProcessing = () => {
  return (
    <Flex flexDirection={"column"} justifyContent={"center"} alignItems={"center"} gap={THEME.SPACING.MEDIUM}>
      <h2>{TextResources.REGISTER_PROCESSING}</h2>
      <Spinner />
    </Flex>
  );
};
