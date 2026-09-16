import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { IngestComponent } from './components/ingest/ingest.component';
import { AskComponent } from './components/ask/ask.component';
import { WorkflowComponent } from './components/workflow/workflow.component';
import { ApprovalQueueComponent } from './components/approval-queue/approval-queue.component';
import { TraceViewerComponent } from './components/trace-viewer/trace-viewer.component';
import { SpendViewComponent } from './components/spend-view/spend-view.component';

export const routes: Routes = [
  { path: 'ingest', component: IngestComponent, canActivate: [authGuard] },
  { path: 'ask', component: AskComponent, canActivate: [authGuard] },
  { path: 'workflow', component: WorkflowComponent, canActivate: [authGuard] },
  { path: 'approval-queue', component: ApprovalQueueComponent, canActivate: [authGuard] },
  { path: 'trace-viewer', component: TraceViewerComponent, canActivate: [authGuard] },
  { path: 'spend-view', component: SpendViewComponent, canActivate: [authGuard] },
  { path: '', redirectTo: '/ask', pathMatch: 'full' }
];
