import "@testing-library/jest-dom";
import { cleanup, render, screen } from "@testing-library/react";
import TyleThemeProvider from "components/TyleThemeProvider";
import { afterEach, describe, expect } from "vitest";
import ExploreSection from "./ExploreSection";

const setup = () => {
  const testComponent = render(
    <TyleThemeProvider theme={"light"}>
      <ExploreSection title={"test"}>
        <></>
      </ExploreSection>
    </TyleThemeProvider>,
  );
  const explorerHeader = screen.getByText("test");

  return {
    explorerHeader,
    ...testComponent,
  };
};
describe("Explorer section unit test", () => {
  afterEach(() => {
    cleanup();
  });
  test("Did component render", () => {
    const { explorerHeader } = setup();
    expect(explorerHeader).toBeInTheDocument();
  });
});
