import { ReactNode } from "react";
import { UnauthenticatedContentContainer, UnauthenticatedContentSection } from "./UnauthenticatedContent.styled";
import { UnauthenticatedContentHeader, UnauthenticatedContentHeaderProps } from "./UnauthenticatedContentHeader";

export type UnauthenticatedContentProps = UnauthenticatedContentHeaderProps & {
  firstRow: ReactNode;
  secondRow?: ReactNode;
};

/**
 * Component that facilitates common functionality that are often used in unauthenticated views
 *
 * @param props
 * @constructor
 */
export const UnauthenticatedContent = (props: UnauthenticatedContentProps) => {
  const { title, subtitle, firstRow, secondRow } = props;
  const showHeader = title || subtitle;

  return (
    <UnauthenticatedContentContainer>
      {showHeader && <UnauthenticatedContentHeader title={title} subtitle={subtitle} />}
      <UnauthenticatedContentSection>{firstRow}</UnauthenticatedContentSection>
      <UnauthenticatedContentSection>{secondRow}</UnauthenticatedContentSection>
    </UnauthenticatedContentContainer>
  );
};
