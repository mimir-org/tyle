import ReactDOM from "react-dom";
import Config from "./models/Config";
import { ApplicationInsights } from "@microsoft/applicationinsights-web";
import { Root } from "./components/root";

if (Config.APP_INSIGHTS_CONNECTION_STRING) {
  const appInsights = new ApplicationInsights({
    config: {
      connectionString: Config.APP_INSIGHTS_CONNECTION_STRING,
    },
  });

  appInsights.loadAppInsights();
  appInsights.trackPageView();
}

ReactDOM.render(<Root />, document.getElementById("root"));
