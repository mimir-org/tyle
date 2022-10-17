import { ReactNode } from "react";
import { useTheme } from "styled-components";
import { Button } from "../../../../../complib/buttons";
import { Heading, Text } from "../../../../../complib/text";
import { Actionable } from "../../../../../complib/types";
import { MotionLogo } from "../../../../common/logo/Logo";
import {
  UnauthenticatedContentContainer,
  UnauthenticatedContentPrimaryContainer,
  UnauthenticatedContentSecondaryContainer,
} from "./UnauthenticatedContent.styled";
import { UnauthenticatedContentHeader, UnauthenticatedContentHeaderProps } from "./UnauthenticatedContentHeader";

type UnauthenticatedContentProps = Partial<Actionable> &
  UnauthenticatedContentHeaderProps & {
    children?: ReactNode;
    infoTitle?: string;
    infoText?: string;
  };

/**
 * Component that facilitates common functionality that are often used in unauthenticated views
 *
 * @param props
 * @constructor
 */
export const UnauthenticatedContent = (props: UnauthenticatedContentProps) => {
  const { children, title, subtitle, infoTitle, infoText, actionable, actionIcon, actionText, onAction } = props;
  const theme = useTheme();
  const showHeader = title || subtitle;
  const showInfoBox = infoTitle && infoText;
  const showAction = actionable && onAction;

  return (
    <UnauthenticatedContentContainer>
      <UnauthenticatedContentPrimaryContainer>
        <MotionLogo layout width={"100px"} height={"50px"} inverse alt="" />
        {showHeader && <UnauthenticatedContentHeader title={title} subtitle={subtitle} />}
        {children}
      </UnauthenticatedContentPrimaryContainer>
      {showInfoBox && (
        <UnauthenticatedContentSecondaryContainer>
          <Heading as={"h2"} variant={"headline-large"}>
            {infoTitle}
          </Heading>
          <Text variant={"body-large"} mt={theme.tyle.spacing.base}>
            {infoText}
          </Text>
          {showAction && (
            <Button variant={"outlined"} alignSelf={"center"} m={"auto 0"} icon={actionIcon} onClick={onAction}>
              {actionText}
            </Button>
          )}
        </UnauthenticatedContentSecondaryContainer>
      )}
    </UnauthenticatedContentContainer>
  );
};
