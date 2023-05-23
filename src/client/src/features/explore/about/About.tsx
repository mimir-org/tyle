import { mapAspectObjectLibCmToAspectObjectItem, mapTerminalLibCmToTerminalItem } from "common/utils/mappers";
import { useGetAspectObject } from "external/sources/aspectobject/aspectObject.queries";
import { useGetTerminal } from "external/sources/terminal/terminal.queries";
import { Loader } from "features/common/loader";
import { AboutPlaceholder } from "features/explore/about/components/AboutPlaceholder";
import { AspectObjectPanel } from "features/explore/about/components/aspectobject/AspectObjectPanel";
import { TerminalPanel } from "features/explore/about/components/terminal/TerminalPanel";
import { ExploreSection } from "features/explore/common/ExploreSection";
import { SelectedInfo } from "features/explore/common/selectedInfo";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

interface AboutProps {
  selected?: SelectedInfo;
}

/**
 * Component which houses info-panels that display various data associated with the selected item.
 *
 * @param selected the currently selected item
 * @constructor
 */
export const About = ({ selected }: AboutProps) => {
  const { t } = useTranslation("explore");

  const aspectObjectQuery = useGetAspectObject(selected?.type === "aspectObject" ? selected?.id : undefined);
  const terminalQuery = useGetTerminal(selected?.type == "terminal" ? selected?.id : undefined);

  const [showLoader, setShowLoader] = useState(true);

  useEffect(() => {
    const allQueries = [aspectObjectQuery, terminalQuery];
    setShowLoader(allQueries.some((x) => x.isFetching));
  }, [aspectObjectQuery, terminalQuery]);

  const showPlaceHolder = !showLoader && selected?.type === undefined;
  const showAspectObjectPanel = !showLoader && selected?.type === "aspectObject" && aspectObjectQuery.isSuccess;
  const showTerminalPanel = !showLoader && selected?.type === "terminal" && terminalQuery.isSuccess;

  return (
    <ExploreSection title={t("about.title")}>
      {showLoader && <Loader />}
      {showPlaceHolder && <AboutPlaceholder text={t("about.placeholders.item")} />}
      {showAspectObjectPanel && (
        <AspectObjectPanel
          key={aspectObjectQuery.data.id + aspectObjectQuery.data.kind}
          {...mapAspectObjectLibCmToAspectObjectItem(aspectObjectQuery.data)}
        />
      )}
      {showTerminalPanel && (
        <TerminalPanel
          key={terminalQuery.data.id + terminalQuery.data.kind}
          {...mapTerminalLibCmToTerminalItem(terminalQuery.data)}
        />
      )}
    </ExploreSection>
  );
};
